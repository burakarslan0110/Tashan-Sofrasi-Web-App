using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TashanSofrasi.DTOLayer.FooterDTO;
using TashanSofrasi.EntityLayer.Entities;

namespace TashanSofrasi.BusinessLayer.ValidationRules.FooterValidations
{
	public class UpdateFooterValidationRules : AbstractValidator<UpdateFooterDTO>
	{
		public UpdateFooterValidationRules()
		{
			RuleFor(x=>x.FooterLocation).NotEmpty().WithMessage("Lokasyon bilgisi boş bırakılamaz.");
			RuleFor(x=>x.FooterPhoneNumber).NotEmpty().WithMessage("Telefon bilgisi boş bırakılamaz.");
			RuleFor(x=>x.FooterEmail).NotEmpty().WithMessage("E-posta bilgisi boş bırakılamaz.");
			RuleFor(x=>x.FooterOpeningHours).NotEmpty().WithMessage("E-posta bilgisi boş bırakılamaz.");
			RuleFor(x=>x.FooterDescription).NotEmpty().WithMessage("Footer açıklama bilgisi boş bırakılamaz.");

			RuleFor(x => x.FooterLocation).Length(10, 75).WithMessage("Lokasyon bilgisi en az 10 karakter en fazla 75 karakter olabilir.");
			RuleFor(x => x.FooterDescription).Length(10, 100).WithMessage("Lokasyon bilgisi en az 10 karakter en fazla 100 karakter olabilir.");
			RuleFor(x => x.FooterEmail).Length(5, 80).WithMessage("E-posta bilgisi en az 5 karakter en fazla 80 karakter olabilir.");
			RuleFor(x => x.FooterOpeningHours).MaximumLength(25).WithMessage("Açılış kapanış saatleri bilgisi en fazla 25 karakter olabilir.");
			RuleFor(x=>x.FooterEmail).EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");
		}
	}

}
