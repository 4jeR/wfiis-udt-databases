using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectSchoolUtils
{
    class Command
    {
        public enum Type
        {
            Null,
            Insert,
            SelectAll,
            SortByPrice
        }

        private String m_Comm;
        private Type m_Type = Type.Null;
        private String m_Prompt;
        List<String> m_ResultList;

        public Command()
        {
        }

        public Command(String command, Type type, String prompt, List<String> r)
        {
            m_Comm = command;
            m_Type = type;
            m_Prompt = prompt;
            m_ResultList = r;
        }

        public String GetCommand() { return m_Comm; }
        public Type GetCommandType() { return m_Type; }
        public String GetPrompt() { return m_Prompt; }
        public List<string> GetResultList() { return m_ResultList; }

    }
}
