using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Mappings
{
    public static class ParameterMapper
    {
        public static ParameterDTO Map(Parameter obj)
        {
            return obj == null
                ? null
                : new ParameterDTO
                {
                    Value = obj.Value,
                    Type = obj.Type
                };
        }
        
        public static Parameter Map(ParameterDTO obj)
        {
            return obj == null
                ? null
                : new Parameter
                {
                    Value = obj.Value,
                    Type = obj.Type
                };
        }
    }
}