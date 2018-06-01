using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neu.MOP.FactorsCalculator
{
    public static class Constant
    {
        public const string OpportunityFactors = "neu_needbusinessdrivers,neu_definitionofrequirements,neu_timescales,neu_budget,neu_accesstobudget,neu_financialbuyer,neu_technicalbuyer,neu_champion,neu_userbuyerscoverage,neu_corporatefit,neu_solutionvision,neu_strategicpurchase";
        public const string RiskFactors = "neu_compellingevent,neu_decisionprocess,neu_presentrelationship,neu_oursolutionfit,neu_ourproofreferences,neu_competitorsposition,neu_paybackroi,neu_costofsale,neu_ongoingrevenue,neu_salesresourcerequired,neu_delivery,neu_timetodeliver";
        public const string EntityNameOpportunityProfile = "neu_opportunityprofile";
        public const string PrimaryKeyOpportunityProfile = "neu_opportunityprofileid";
        public const string AttributeOpportunityFactors = "neu_opportunityfactors";
        public const string AttributeRiskFactors = "neu_riskfactors";
        public const string CreateMessage = "create";
        public const string UpdateMessage = "update";
        public const string PostImage = "PostImage";
        public const string Target = "target";
    }
}
