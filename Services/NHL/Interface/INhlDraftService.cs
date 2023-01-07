﻿using Tietoa.Domain.Models.Draft;

namespace Services.NHL.Interface

{
    public interface INhlDraftService
    {
        Task<List<DraftByYearDto>> GetDraftByYear(int year);
    }
}