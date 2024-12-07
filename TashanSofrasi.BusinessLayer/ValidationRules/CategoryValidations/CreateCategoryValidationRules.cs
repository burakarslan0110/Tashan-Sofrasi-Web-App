using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TashanSofrasi.DTOLayer.CategoryDTO;

namespace TashanSofrasi.BusinessLayer.ValidationRules.CategoryValidations
{
	public class CreateCategoryValidationRules : AbstractValidator<CreateCategoryDTO>
	{
		public CreateCategoryValidationRules()
		{
			RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Kategori adı boş geçilemez.");

			RuleFor(x => x.CategoryName).MinimumLength(3).WithMessage("Kategori adı en az 3 karakter olmalıdır.");

			RuleFor(x => x.CategoryName).MaximumLength(35).WithMessage("Kategori adı en fazla 35 karakter olmalıdır.");
		}
	}
}
