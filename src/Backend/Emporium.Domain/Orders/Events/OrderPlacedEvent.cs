﻿namespace Emporium.Domain.Orders.Events;
public record OrderPlacedEvent(OrderId OrderId) : DomainEventBase;