using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using SimpleInjector;

namespace lagginDragon
{
    public interface IModule
    {
        void Register(Container container);
        void Deregister(Container container);
        string Name { get; }
    }

    // Registry with hot-swap capability
    public class ModuleRegistry
    {
        private readonly Container _container;
        private readonly Dictionary<string, IModule> _loadedModules = new();

        public ModuleRegistry(Container container)
        {
            _container = container;
        }

        // Load a module assembly and register its IModule implementations
        public void LoadModuleFromFile(string dllPath)
        {
            if (!File.Exists(dllPath))
                throw new FileNotFoundException("Module DLL not found", dllPath);

            var assembly = Assembly.LoadFrom(dllPath);

            // Use reflection to discover and instantiate IModule implementations
            var moduleTypes = assembly.GetTypes()
                .Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);

            foreach (var type in moduleTypes)
            {
                var module = (IModule)Activator.CreateInstance(type)!;
                if (_loadedModules.ContainsKey(module.Name))
                {
                    Console.WriteLine($"Module {module.Name} already loaded.");
                    continue;
                }

                module.Register(_container);
                _loadedModules[module.Name] = module;
                Console.WriteLine($"Module {module.Name} registered.");
            }
        }

        // Unload module by name
        public void UnloadModule(string name)
        {
            if (_loadedModules.TryGetValue(name, out var module))
            {
                module.Deregister(_container);
                _loadedModules.Remove(name);
                Console.WriteLine($"Module {name} unloaded.");
            }
        }

        // Resolve service through SimpleInjector
        public T GetService<T>() where T : class
        {
            return _container.GetInstance<T>();
        }
    }
}