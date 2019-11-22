using System.Collections.Generic;
using UnityEngine;
public interface Comparable<T> {
    bool Equals( T b );
}

public interface Rule<In,Out> where In : Comparable<In> {
    Out Process( In input );
}

public class Tape : List<int>, Comparable<Tape> {
    public bool Equals( Tape other ){
        int length = this.Count > other.Count ? other.Count : this.Count ;
        for( int i = 0 ; i < length ; i++ ){
            if( this[i] != other[i] ){
                return false;
            }
        } 
        return true;
    }
    public int CircularGet(int rawIndex){
        int index = rawIndex % this.Count;
        return this[index];
    }
    public Tape Slice(int start, int end) {
        Tape slice = new Tape();
        for( int i = start ; i < end ; i++ ){
            slice.Add(this.CircularGet(i));
        }
        return slice;
    }
}

public class SimpleRule : Rule<Tape,int> {
    public Tape input;
    public int output;
    public System.Func<int> gen;
    public int length;
    public SimpleRule( string pattern, int o ){
        Tape input = new Tape();
        foreach( char c in pattern ){
            input.Add((int)System.Char.GetNumericValue(c));
        }
        this.input = input;
        this.output = o;
        this.length = pattern.Length;
    }
    public SimpleRule( string pattern, System.Func<int> o){
        Tape input = new Tape();
        foreach( char c in pattern ){
            input.Add((int)System.Char.GetNumericValue(c));
        }
        this.input = input;
        this.gen = o;
        this.length = pattern.Length;
    }
    public int Process( Tape input ){
        return this.input.Equals(input) ? GetOutput() : -1;
    }

    public int GetOutput() {
        return gen == null ? output : gen() ;
    }
}

public class PathGenerator {
    List<Rule<Tape,int>> rules;
    public PathGenerator(){
        rules = new List<Rule<Tape,int>>();
    }
    public void AddRule( Rule<Tape,int> rule ){
        this.rules.Add(rule);
    }
    public int Process( Tape t , int index, bool debug = false ){
        int output = -1;
        foreach( SimpleRule r in rules ) {
            if( output == -1 ){
                output = r.Process(t.Slice( index , index + r.length ));
                if( debug){
                    Debug.Log(
                        "Rule : " + tapeString(r.input) + "\n" +
                        "In   : " + tapeString(t.Slice( index , index + r.length )) + "\n" +
                        "Out  : " + output.ToString()
                    );
                }
            }
        }
        return output == -1 ? t[index] : output;
    }

    public List<int> PostProcess( Tape tape ){
        List<int> t = new List<int>(tape);
        PathGenerator post = new PathGenerator();
        post.AddRule(new SimpleRule("2525", () => Random.Range(1,7)));
        post.AddRule(new SimpleRule("777", () => Random.Range(1,6)));
        post.AddRule(new SimpleRule("222", () => Random.Range(1,6)));
        post.AddRule(new SimpleRule("22", () => Random.Range(1,6)));
        post.AddRule(new SimpleRule("333", () => Random.Range(1,6)));
        post.AddRule(new SimpleRule("33", () => Random.Range(1,6)));
        post.AddRule(new SimpleRule("444", () => Random.Range(1,6)));
        post.AddRule(new SimpleRule("44", () => Random.Range(1,6)));
        post.AddRule(new SimpleRule("555", () => Random.Range(1,6)));
        post.AddRule(new SimpleRule("55", () => Random.Range(1,6)));
        post.AddRule(new SimpleRule("000", () => Random.Range(1,7)));
        post.AddRule(new SimpleRule("00", () => Random.Range(1,6)));
        post.AddRule(new SimpleRule("07", () => Random.Range(1,7)));
        post.AddRule(new SimpleRule("0", () => Random.Range(1,7)));
        for(int i = 0 ; i < t.Count ; i++ ){
            t[i] = post.Process(tape,i);
        }
        t[0] = 0;
        return t;
    }

    public string tapeString<T>( T t ) where T : List<int> {
        string str = "[";
        foreach( int i in t ){
            str += i.ToString() + ",";
        }
        return str + "]";
    }
    public List<int> Generate(int amount = 10 , bool debug = false){
        Tape tape = new Tape();
        Tape result = new Tape();
        for( int i = 0 ; i < amount ; i++ ){
            tape.Add(Random.Range(0,2));
        }
        for( int i = 0 ; i < amount ; i++ ){
            result.Add(Process(tape,i));
        }
        List<int> res = PostProcess(result);
        if( debug ){
            Debug.Log( 
                tapeString(tape) + " => \n" + 
                tapeString(result) + " => \n" +
                tapeString(res) 
            );
        }
        return res;
    }

    public static PathGenerator ThreeBitGenerator(){
        PathGenerator gen = new PathGenerator();
        gen.AddRule(new SimpleRule("000",0));
        gen.AddRule(new SimpleRule("100",1));
        gen.AddRule(new SimpleRule("010",2));
        gen.AddRule(new SimpleRule("110",3));
        gen.AddRule(new SimpleRule("001",4));
        gen.AddRule(new SimpleRule("101",5));
        gen.AddRule(new SimpleRule("011",6));
        gen.AddRule(new SimpleRule("111",7));
        return gen;
    }

    public static Queue<int> GenerateQueue(){
        return new Queue<int>(ThreeBitGenerator().Generate());
    }
}