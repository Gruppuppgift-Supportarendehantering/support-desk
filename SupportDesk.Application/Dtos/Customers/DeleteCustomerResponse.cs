using System;
using System.Collections.Generic;
using System.Text;

namespace SupportDesk.Application.Dtos.Customers;

public record DeleteCustomerResponse
    (
        bool Succeeded,
        string? ErrorMessage
    );
