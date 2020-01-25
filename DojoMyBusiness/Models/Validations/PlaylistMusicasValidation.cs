using FluentValidation;

namespace DojoMyBusiness.Models.Validations
{
    public class PlaylistMusicasValidation : AbstractValidator<PlaylistMusica>
    {
        public PlaylistMusicasValidation()
        {
            RuleFor(p => p.MusicaId)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(p => p.PlaylistId)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}