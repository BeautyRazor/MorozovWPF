using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Prism.Mvvm;

namespace MorozovWPF.ViewModels {
    public class ViewModelBase : BindableBase {
        private class PropertyData {
            public object DataObject { get; }
            public PropertyInfo Property { get; }
            public Func<object, object> Converter { get; }
            public PropertyData(object dataObject, PropertyInfo property, Func<object, object> converter = null) {
                DataObject = dataObject;
                Property = property;
                Converter = converter;
            }
        }

        private class BindedProperty {
            public PropertyData SourceProperty { get; }
            public List<PropertyData> DependedProperties { get; }

            public BindedProperty(object dataObject, PropertyInfo property) {
                DependedProperties = new List<PropertyData>();
                SourceProperty = new PropertyData(dataObject, property);
            }
        }

        private class MethodCacheData {
            public WeakReference DataObject { get; }
            public WeakReference Method { get; }
            public MethodCacheData(object dataObject, MethodInfo method) {
                DataObject = new WeakReference(dataObject);
                Method = new WeakReference(method);
            }
        }

        private List<BindedProperty> bindedProperties = new List<BindedProperty>();
        private List<MethodCacheData> cachedMethods = new List<MethodCacheData>();
        private int cacheHitsCount = 0;

        protected void BindProperties(object model, string modelProperty, string viewModelProperty, Func<object, object> toViewModelConverter = null, Func<object, object> toModelConverter = null) {
            INotifyPropertyChanged castedModel = model as INotifyPropertyChanged;
            if (castedModel == null) {
                Debug.Assert(castedModel != null, "Model object doesn't implement INotifyPropertyChanged!");
                return;
            }
            INotifyPropertyChanged castedViewModel = this;

            PropertyInfo modelPropertyInfo = GetPropertyInfo(model, modelProperty);
            PropertyInfo viewModelPropertyInfo = GetPropertyInfo(castedViewModel, viewModelProperty);

            if (modelProperty == null || viewModelProperty == null) {
                Debug.Assert(modelPropertyInfo     != null, "Model doesn't have public property with name "     + modelProperty);
                Debug.Assert(viewModelPropertyInfo != null, "ViewModel doesn't have public property with name " + viewModelProperty);
                return;
            }

            if (toViewModelConverter == null && toModelConverter == null) {
                bool isEqualTypes = CheckPropertyTypesEquals(modelPropertyInfo, viewModelPropertyInfo);
                if (!isEqualTypes) {
                    Debug.Assert(isEqualTypes, "Properties types doesn't equals!");
                    return;
                }
            }

            if (bindedProperties.Count(p => ReferenceEquals(p.SourceProperty.DataObject, model)) == 0) {
                castedModel.PropertyChanged += BindedPropertyChanged;
            }
            if (bindedProperties.Count(p => ReferenceEquals(p.SourceProperty.DataObject, castedViewModel)) == 0) {
                castedViewModel.PropertyChanged += BindedPropertyChanged;
            }

            AddDependency(model, modelPropertyInfo, castedViewModel, viewModelPropertyInfo, toViewModelConverter);
            AddDependency(castedViewModel, viewModelPropertyInfo, model, modelPropertyInfo, toModelConverter);
            UpdatePropertiesValue(model, modelProperty);
        }

        private void AddDependency(
            object sourceDataObject, PropertyInfo sourceProperty, 
            object dependentObject, PropertyInfo dependedProperty, 
            Func<object, object> converter
            ) {
            BindedProperty sourceBindedProperty = bindedProperties
                .FirstOrDefault(p => 
                    p.SourceProperty.DataObject == sourceDataObject 
                    &&
                    p.SourceProperty.Property.Name == sourceProperty.Name);

            if (sourceBindedProperty == null) {
                sourceBindedProperty = new BindedProperty(sourceDataObject, sourceProperty);
                bindedProperties.Add(sourceBindedProperty);
            }

            PropertyData dependedPropertyData = sourceBindedProperty.DependedProperties
                .FirstOrDefault(p =>
                    p.DataObject == dependentObject 
                    &&
                    p.Property.Name == dependedProperty.Name);

            if (dependedPropertyData != null) {
                return;
            }
            dependedPropertyData = new PropertyData(dependentObject, dependedProperty, converter);
            sourceBindedProperty.DependedProperties.Add(dependedPropertyData);
        }

        private PropertyInfo GetPropertyInfo(object obj, string propertyName) {
            PropertyInfo result = obj.GetType().GetProperty(propertyName);
            Debug.Assert(result != null, "Property " + propertyName + " didn't found!");
            if (result != null) {
                Debug.Assert(result.CanRead, "Property " + propertyName + " can't be readed!");
            }
            return result;
        }

        private bool CheckPropertyTypesEquals(PropertyInfo property1, PropertyInfo property2) {
            if (property1 == null || property2 == null) {
                return false;
            }
            if (property1.PropertyType.FullName == property2.PropertyType.FullName) {
                return true;
            }
            return property1.PropertyType.IsAssignableFrom(property2.PropertyType)
                   &&
                   property2.PropertyType.IsAssignableFrom(property1.PropertyType);
        }

        private object GetPropertyValue(object dataObject, PropertyInfo property) => 
            property.CanRead ? property.GetValue(dataObject) : null;

        private void NotifyPropertyChanged(object dataObject, PropertyInfo property) {
            BindableBase notificationObject = dataObject as BindableBase;
            if (notificationObject == null) {
                Debug.Assert(notificationObject != null, "Data object doesn't implement INotifyPropertyChanged!");
                return;
            }
            MethodInfo raisePropertyChangedMethod = GetRaisePropertyChangedMethod(notificationObject);

            if (raisePropertyChangedMethod == null) {
                Debug.Assert(raisePropertyChangedMethod != null, "Can't find RaisePropertyChangedEvent!");
                return;
            }
            raisePropertyChangedMethod.Invoke(notificationObject, new object[] { property.Name });
        }

        private MethodInfo GetRaisePropertyChangedMethod(object dataObject) {
            MethodInfo result = null;
            result = TryGetRaisePropertyChangedMethodFromCache(dataObject);
            if (result != null) {
                return result;
            }

            Type type = dataObject.GetType();
            while (result == null && type != null) {
                result =  type.GetMethod("RaisePropertyChanged", BindingFlags.Instance | BindingFlags.NonPublic);
                type = type.BaseType;
            }
            if (result != null) {
                cachedMethods.Add(new MethodCacheData(dataObject, result));
            }
            return result;
        }

        private MethodInfo TryGetRaisePropertyChangedMethodFromCache(object dataObject) {
            MethodInfo result = null;
            MethodCacheData data = cachedMethods.FirstOrDefault(d => d.DataObject.Target == dataObject);

            if (data != null && data.DataObject.IsAlive && data.Method.IsAlive) {
                result = data.Method.Target as MethodInfo;
            }
            RemoveDeadDataObjects();

            return result;
        }

        private void RemoveDeadDataObjects() {
            if (cacheHitsCount < 10) {
                cacheHitsCount++;
                return;
            }
            cachedMethods.RemoveAll(m => !m.DataObject.IsAlive);
            cacheHitsCount = 0;
        }

        private void SetPropertyValue(object dataObject, PropertyInfo property, object newValue) {
            Debug.Assert(property.CanWrite, "Property " + property.Name + " can't be written!");
            if (!property.CanWrite) {
                return;
            }

            Type assigningType = newValue?.GetType();
            bool canBeAssigned = assigningType == null || property.PropertyType.IsInstanceOfType(newValue);

            if (canBeAssigned) {
                Debug.Assert(canBeAssigned, "Property " + property.Name + " can't be set!");
                property.SetValue(dataObject, newValue);
            }
        }

        private void BindedPropertyChanged(object sender, PropertyChangedEventArgs e) {
            UpdatePropertiesValue(sender, e.PropertyName);
        }

        private void UpdatePropertiesValue(object sourceDataObject, string sourcePropertyName) {
            BindedProperty sourceBindedProperty = bindedProperties
                .FirstOrDefault(p => 
                    p.SourceProperty.DataObject == sourceDataObject 
                    && 
                    p.SourceProperty.Property.Name == sourcePropertyName);

            if (sourceBindedProperty == null) {
                return;
            }
            PropertyData sourcePropertyData = sourceBindedProperty.SourceProperty;
            object sourceNewValue = GetPropertyValue(sourcePropertyData.DataObject, sourcePropertyData.Property);
            foreach (PropertyData dependedProperty in sourceBindedProperty.DependedProperties) {
                UpdatePropertyValue(sourceNewValue, dependedProperty);
            }
        }

        private void UpdatePropertyValue(object sourceNewValue, PropertyData dependedProperty) {
            object dependedOldValue = GetPropertyValue(dependedProperty.DataObject, dependedProperty.Property);
            object dependedNewValue = sourceNewValue;
            if (dependedProperty.Converter != null) {
                dependedNewValue = dependedProperty.Converter(sourceNewValue);
            }
            if (!object.Equals(dependedOldValue, dependedNewValue)) {
                if (dependedProperty.Property.CanWrite) {
                    SetPropertyValue(dependedProperty.DataObject, dependedProperty.Property, dependedNewValue);
                }
                NotifyPropertyChanged(dependedProperty.DataObject, dependedProperty.Property);
            }
        }
    }
}