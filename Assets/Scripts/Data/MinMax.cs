public class MinMax<T> {
    private T min;
    public T Min {
        get { return min; }
        set { min = value; }
    }
    private T max;
    public T Max {
        get { return max; }
        set { max = value; }
    }
    public MinMax( T min , T max ){
        this.min = min;
        this.max = max;
    }
}