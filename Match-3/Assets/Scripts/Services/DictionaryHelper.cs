using System;
using System.Collections.Generic;

public static class DictionaryHelper {
    public static Dictionary<TKey, TValue> Clone<TKey, TValue>(Dictionary<TKey, TValue> original) where TValue : ICloneable {
        Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>(original.Count, original.Comparer);

        foreach (KeyValuePair<TKey, TValue> entry in original) {
            result.Add(entry.Key, (TValue)entry.Value.Clone());
        }

        return result;
    }
}
