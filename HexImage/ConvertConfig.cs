using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace HexImage;

internal class ConvertConfig : IConfiguration {
    private IConfigurationRoot root;

    public ConvertConfig() {
        root = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false, true)
            .Build();
    }

    public IConfigurationSection GetSection(string key) {
        return root.GetSection(key);
    }

    public IEnumerable<IConfigurationSection> GetChildren() {
        return root.GetChildren();
    }

    public IChangeToken GetReloadToken() {
        return root.GetReloadToken();
    }

    public string? this[string key] {
        get => root[key];
        set => root[key] = value;
    }
}