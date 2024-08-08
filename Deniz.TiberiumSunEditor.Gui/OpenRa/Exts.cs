#region Copyright & License Information
/*
 * Copyright (c) The OpenRA Developers and Contributors
 * This file is part of OpenRA, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */
#endregion


namespace Deniz.TiberiumSunEditor.Gui.OpenRa
{
	public static class Exts
	{

		public static Dictionary<TKey, TSource> ToDictionaryWithConflictLog<TSource, TKey>(
			this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
			string debugName, Func<TKey, string> logKey, Func<TSource, string> logValue)
		{
			return ToDictionaryWithConflictLog(source, keySelector, x => x, debugName, logKey, logValue);
		}

		public static Dictionary<TKey, TElement> ToDictionaryWithConflictLog<TSource, TKey, TElement>(
			this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector,
			string debugName, Func<TKey, string> logKey = null, Func<TElement, string> logValue = null)
		{
			// Fall back on ToString() if null functions are provided:
			logKey ??= s => s.ToString();
			logValue ??= s => s.ToString();

			// Try to build a dictionary and log all duplicates found (if any):
			var dupKeys = new Dictionary<TKey, List<string>>();
			var capacity = source is ICollection<TSource> collection ? collection.Count : 0;
			var d = new Dictionary<TKey, TElement>(capacity);
			foreach (var item in source)
			{
				var key = keySelector(item);
				var element = elementSelector(item);

				// Discard elements with null keys
				if (!typeof(TKey).IsValueType && key == null)
					continue;

				// Check for a key conflict:
				if (!d.TryAdd(key, element))
				{
					if (!dupKeys.TryGetValue(key, out var dupKeyMessages))
					{
						// Log the initial conflicting value already inserted:
						dupKeyMessages = new List<string>
						{
							logValue(d[key])
						};
						dupKeys.Add(key, dupKeyMessages);
					}

					// Log this conflicting value:
					dupKeyMessages.Add(logValue(element));
				}
			}

			// If any duplicates were found, throw a descriptive error
			if (dupKeys.Count > 0)
			{
				var badKeysFormatted = string.Join(", ", dupKeys.Select(p => $"{logKey(p.Key)}: [{string.Join(",", p.Value)}]"));
				var msg = $"{debugName}, duplicate values found for the following keys: {badKeysFormatted}";
				throw new ArgumentException(msg);
			}

			// Return the dictionary we built:
			return d;
		}

        public static V GetOrAdd<K, V>(this Dictionary<K, V> d, K k, Func<K, V> createFn)
        {
            // Cannot use CollectionsMarshal.GetValueRefOrAddDefault here,
            // the creation function could mutate the dictionary which would invalidate the ref.
            if (!d.TryGetValue(k, out var ret))
                d.Add(k, ret = createFn(k));
            return ret;
        }
    }

}
