using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TashanSofrasi.DTOLayer.AboutDTO;

namespace TashanSofrasi.BusinessLayer.ValidationRules.AboutValidations
{
    public class UpdateAboutValidationRules : AbstractValidator<UpdateAboutDTO>
    {
        public UpdateAboutValidationRules()
        {
            RuleFor(x => x.AboutTitle).NotEmpty().WithMessage("Hakkımda başlığı boş geçilemez.");
            RuleFor(x => x.AboutDescription).NotEmpty().WithMessage("Hakkımda içeriği boş geçilemez.");
            RuleFor(x => x.AboutImageURL).NotEmpty().WithMessage("Hakkımda görseli boş geçilemez.");

            RuleFor(x=>x.AboutTitle).MinimumLength(5).WithMessage("Hakkımda başlığı en az 5 karakter olmalıdır.");
            RuleFor(x => x.AboutDescription).MinimumLength(10).WithMessage("Hakkımda içeriği en az 10 karakter olmalıdır.");

            RuleFor(x => x.AboutTitle).MaximumLength(50).WithMessage("Hakkımda başlığı en fazla 50 karakter olmalıdır.");
            RuleFor(x => x.AboutDescription).MaximumLength(1100).WithMessage("Hakkımda içeriği en fazla 1100 karakter olmalıdır.");


        }
    }
}
