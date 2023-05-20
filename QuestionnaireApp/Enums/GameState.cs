using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireApp
{
    public enum AppState
    {
        unknown,
        starting,
        loading,
        answering,
        answered,
        ending
    }
}
