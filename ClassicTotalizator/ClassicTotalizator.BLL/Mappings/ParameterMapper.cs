using ClassicTotalizator.BLL.Contracts;
using ClassicTotalizator.DAL.Entities;

namespace ClassicTotalizator.BLL.Mappings
{
    public static class ParameterMapper
    {
        public static ParameterDTO Map(Parameter parameter)
        {
            return parameter == null
                ? null
                : new ParameterDTO
                {
                    Value = parameter.Value,
                    Type = parameter.Type
                };
        }
        
        public static Parameter Map(ParameterDTO parameter)
        {
            return parameter == null
                ? null
                : new Parameter
                {
                    Value = parameter.Value,
                    Type = parameter.Type
                };
        }
    }
}