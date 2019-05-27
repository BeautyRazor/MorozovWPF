using Prism.Mvvm;

namespace MorozovWPF.Models
{
    public abstract class ModelBase<T> : BindableBase
        where T : ModelBase<T>, new() {
        private static T instance;
        public static T Instance => instance ?? (instance = new T());

        public static void Clear() => instance = null;
    }
}
