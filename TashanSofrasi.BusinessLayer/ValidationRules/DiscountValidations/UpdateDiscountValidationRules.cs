using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TashanSofrasi.DTOLayer.DiscountDTO;

namespace TashanSofrasi.BusinessLayer.ValidationRules.DiscountValidations
{
    public class UpdateDiscountValidationRules : AbstractValidator<UpdateDiscountDTO>
    {
        public UpdateDiscountValidationRules()
        {
            RuleFor(x=>x.DiscountTitle).NotEmpty().WithMessage("İndirim başlığı boş geçilemez.");
            RuleFor(x=>x.DiscountAmount).NotEmpty().WithMessage("İndirim miktarı boş geçilemez.");
            RuleFor(x=>x.DiscountDescription).NotEmpty().WithMessage("İndirim açıklaması boş geçilemez.");

            RuleFor(x => x.DiscountTitle).MinimumLength(3).WithMessage("İndirim başlığı en az 3 karakter olmalıdır.");
            RuleFor(x => x.DiscountTitle).MaximumLength(30).WithMessage("İndirim başlığı en fazla 30 karakter olmalıdır.");

            RuleFor(x => x.DiscountDescription).MinimumLength(10).WithMessage("İndirim açıklaması en az 10 karakter olmalıdır.");
            RuleFor(x => x.DiscountDescription).MaximumLength(45).WithMessage("İndirim açıklaması en fazla 45 karakter olmalıdır.");

            RuleFor(x => x.DiscountAmount).InclusiveBetween(1, 100).WithMessage("İndirim miktarı 1 ile 100 arasında olmalıdır.");



        }
    }
}
