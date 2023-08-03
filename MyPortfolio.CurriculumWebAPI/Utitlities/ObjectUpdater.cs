using CurriculumWebAPI.App.InputModels;
using CurriculumWebAPI.Domain.Models.ComplexTypes;
using CurriculumWebAPI.Domain.Models.CurriculumBody;
using System.Reflection;

namespace CurriculumWebAPI.App.Utitlities
{
    public static class ObjectUpdater
    {
        public static void UpdateObject<TExisting, TUpdated>(TExisting existingObject, TUpdated updatedObject)
        {
            if (existingObject == null || updatedObject == null)
            {
                return;
            }

            Type existingType = typeof(TExisting);
            Type updatedType = typeof(TUpdated);

            PropertyInfo[] existingProperties = existingType.GetProperties();
            PropertyInfo[] updatedProperties = updatedType.GetProperties();

            foreach (var updatedProperty in updatedProperties)
            {
                var existingProperty = existingProperties.FirstOrDefault(p => p.Name == updatedProperty.Name);
                if (existingProperty == null || !existingProperty.CanWrite)
                {
                    continue;
                }

                var existingValue = existingProperty.GetValue(existingObject);
                var updatedValue = updatedProperty.GetValue(updatedObject);

                if (existingValue != null && existingValue.Equals(updatedValue))
                {
                    continue; // Skip if the values are the same
                }

                existingProperty.SetValue(existingObject, updatedValue);
            }
        }


        public static PhoneNumber GetPhoneNumberFromInputModel(ContatoInputModel inputModel)
        {
            return new PhoneNumber
            {
                Codigo = inputModel.Codigo,
                DDD = inputModel.DDD,
                Numero = inputModel.NumeroTelefone_Celular.ToString()
            };
        }

        // Método auxiliar para obter Address a partir do ContatoInputModel
        public static Address GetAddressFromInputModel(ContatoInputModel inputModel)
        {
            return new Address
            {
                Rua = inputModel.Rua,
                Bairro = inputModel.Bairro,
                NumeroCasa = inputModel.NumeroCasa,
                Cidade = inputModel.Cidade,
                Estado = inputModel.Estado
            };
        }
    }
}
