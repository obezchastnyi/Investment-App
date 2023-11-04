﻿using System;

namespace InvestmentApp.V1.DTOs.Projects;

public class ProjectDto
{
    public string Name { get; set; }

    public double StartingInvestmentSum { get; set; }

    public Guid EnterpriseId { get; set; }
}
