namespace Seabites.ChangeOverTime {
  public class CSharpMethodChange {
    readonly ChangeType _typeOfChange;
    readonly CSharpMethod _method;

    public CSharpMethodChange(ChangeType typeOfChange, CSharpMethod method) {
      _typeOfChange = typeOfChange;
      _method = method;
    }

    public ChangeType ChangeType {
      get {
        return _typeOfChange;
      }
    }

    public CSharpMethod Method {
      get {
        return _method;
      }
    }
  }
}