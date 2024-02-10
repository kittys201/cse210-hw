

public abstract class Goal
{
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _description = description;
        _points = points;
    }

    public abstract void RecordEvent();

    public abstract bool IsComplete();

    public string GetStringRepresentation()
    {
        return $"{GetShortName()},{_description},{_points}";
    }

    public int GetPoints()
    {
        return _points;
    }

    public abstract string GetShortName();
}

public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points) : base(name, description, points)
    {
        _isComplete = false;
    }

    public override void RecordEvent()
    {
        if (!_isComplete)
            _isComplete = true;
    }

    public override bool IsComplete()
    {
        return _isComplete;
    }

    public override string GetShortName()
    {
        return _description.Substring(0, Math.Min(5, _description.Length));
    }
}

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points)
    {
    }

    public override void RecordEvent()
    {
       
    }

    public override bool IsComplete()
    {
        return false;
    }

    public override string GetShortName()
    {
        return _description.Substring(0, Math.Min(5, _description.Length));
    }
}

public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int target, int bonus) : base(name, description, points)
    {
        _amountCompleted = 0;
        _target = target;
        _bonus = bonus;
    }

    public override void RecordEvent()
    {
        _amountCompleted++;
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

    public override string GetShortName()
    {
        return _description.Substring(0, Math.Min(5, _description.Length));
    }

    public new int GetPoints()
    {
        if (IsComplete())
            return _points + _bonus;
        else
            return _points;
    }
}
