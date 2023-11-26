using System;

public delegate void PropertyEventHandler(object sender, PropertyChangedEventArgs e);

public class PropertyChangedEventArgs : EventArgs
{
    public string PropertyName { get; }

    public PropertyChangedEventArgs(string propertyName)
    {
        PropertyName = propertyName;
    }
}

public interface IPropertyChanged
{
    event PropertyEventHandler PropertyChanged;
}
public class ExampleClass : IPropertyChanged
{
    private string _name;

    public event PropertyEventHandler PropertyChanged;

    public string Name
    {
        get { return _name; }
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

class Program
{
    static void Main()
    {
        ExampleClass example = new ExampleClass();

        example.PropertyChanged += Example_PropertyChanged;

        example.Name = "NewName";

        Console.ReadLine();
    }

    private static void Example_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        Console.WriteLine($"Property '{e.PropertyName}' has been changed.");
    }
}

