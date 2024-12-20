namespace Ordering.Application.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(Guid OrderId) : ICommand<DeleteOrderResult>;

public record DeleteOrderResult(bool IsSuccess);

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(p => p.OrderId ).NotEmpty().WithMessage("OrderId is required");
    }
}