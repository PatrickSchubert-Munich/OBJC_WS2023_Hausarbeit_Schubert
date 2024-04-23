using System.Xml.Serialization;

namespace Werkzeugverleih.Classes
{
    /// <summary>
    /// Select tool which lends a customer
    /// </summary>
    public class LendingTool
    {
        private List<int> _toolIds;
        private List<Tool> ListOfTools = new List<Tool>();
        private ToolManagement _toolManagement;

        /// <summary>
        /// Constructor of lending tools
        /// </summary>
        /// <param name="toolIds">Tool Id</param>
        /// <param name="toolManagement">Instance of tool Management class</param>
        public LendingTool(List<int> toolIds, ToolManagement toolManagement)
        {
            _toolIds = toolIds;
            _toolManagement = toolManagement;
        }

        public List<int> ToolIds
        {
            get
            {
                return _toolIds;
            }
        }

        [XmlIgnore]
        public List<Tool> Tool
        {
            get
            {
                var tools = _toolManagement.GetItem();

                foreach (var toolId in _toolIds)
                {
                    foreach (var tool in tools)
                    {
                        if (tool.ToolId == toolId)
                        {
                            ListOfTools.Add(tool);
                        }
                    }
                }
                return ListOfTools;
            }
        }
    }
}
