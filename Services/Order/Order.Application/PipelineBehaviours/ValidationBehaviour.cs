using FluentValidation;
using MediatR;

namespace ESouring.Ordering.Application.PipelineBehaviours
{
    public sealed class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, MediatR.IRequest<TResponse>
    {

        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour()
        {
            _validators = new List<IValidator<TRequest>>();
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {

            var context = new ValidationContext<TRequest>(request);
            var failures = _validators.Select(x => x.Validate(context))
                                      .SelectMany(x => x.Errors)
                                      .Where(x => x != null)
                                      .ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            return next();
        }
    }
}
