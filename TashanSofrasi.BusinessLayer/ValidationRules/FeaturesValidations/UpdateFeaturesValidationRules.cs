using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TashanSofrasi.DTOLayer.FeatureDTO;

namespace TashanSofrasi.BusinessLayer.ValidationRules.FeaturesValidations
{
    public class UpdateFeaturesValidationRules : AbstractValidator<UpdateFeatureDTO>
    {
        public UpdateFeaturesValidationRules()
        {
            RuleFor(x=>x.FeatureTitle1).NotEmpty().WithMessage("Öne çıkan başlığı boş geçilemez");
            RuleFor(x=>x.FeatureTitle2).NotEmpty().WithMessage("Öne çıkan başlığı boş geçilemez");
            RuleFor(x=>x.FeatureTitle3).NotEmpty().WithMessage("Öne çıkan başlığı boş geçilemez");

            RuleFor(x => x.FeatureTitle1).MinimumLength(5).WithMessage("Öne çıkan başlığı en az 5 karakter olmalıdır");
            RuleFor(x => x.FeatureTitle2).MinimumLength(5).WithMessage("Öne çıkan başlığı en az 5 karakter olmalıdır");
            RuleFor(x => x.FeatureTitle3).MinimumLength(5).WithMessage("Öne çıkan başlığı en az 5 karakter olmalıdır");

            RuleFor(x => x.FeatureTitle1).MaximumLength(35).WithMessage("Öne çıkan başlığı en fazla 35 karakter olmalıdır");
            RuleFor(x => x.FeatureTitle2).MaximumLength(35).WithMessage("Öne çıkan başlığı en fazla 35 karakter olmalıdır");
            RuleFor(x => x.FeatureTitle3).MaximumLength(35).WithMessage("Öne çıkan başlığı en fazla 35 karakter olmalıdır");
        }
    }
}
