namespace Ingresso.Application.CodeRandomUser
{
    public class CodeRandomDictionary
    {
        private readonly Dictionary<string, int> DictionaryCode = new();

        public void Add(string idGuid, int valueCode)
        {
            int seila;
            if (!DictionaryCode.TryGetValue(idGuid, out seila))
            {
                DictionaryCode.Add(idGuid, valueCode);
            }
            else
            {
                DictionaryCode[idGuid] = valueCode;
            }

        }

        public bool Container(string guidId, int valueCode)
        {
            if (DictionaryCode.ContainsKey(guidId))
            {
                int value;
                if (DictionaryCode.TryGetValue(guidId, out value))
                {
                    if (value == valueCode)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool Container(string guidId)
        {
            if (DictionaryCode.ContainsKey(guidId))
            {
                return true;
            }

            return false;
        }

        public void Remove(string idGuid)
        {
            if (DictionaryCode.ContainsKey(idGuid))
            {
                DictionaryCode.Remove(idGuid);
            }
        }
    }
}
