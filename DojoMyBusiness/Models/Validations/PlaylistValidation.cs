using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace DojoMyBusiness.Models.Validations
{
    public class PlaylistValidation : AbstractValidator<Playlist>
    {
        public PlaylistValidation()
        {
            RuleFor(p => p.Id)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleForEach(p => p.PlaylistMusicas).SetValidator(new PlaylistMusicasValidation());

            RuleFor(p => p.PlaylistMusicas).Must(pm => !IsDuplicate(pm))
                .WithMessage("Não é permitido musicas repetidas na playlist");

        }

        private static bool IsDuplicate(IEnumerable<PlaylistMusica> musicas)
        {
            return musicas.GroupBy(m => m.MusicaId).Any(m => m.Count() > 1);
        }
    }
}
