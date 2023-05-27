using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;

namespace TEMA_4_DZ_Final
{
    [Transaction(TransactionMode.Manual)]
    public class AddAutoNumbering : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;
            List<Room> rooms = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Rooms)
                .OfType<Room>()
                .ToList();

            Transaction transaction = new Transaction(doc);
            transaction.Start("Автоматическая Нумерация помещений");
            for (int i = 0; i < rooms.Count; i++)
            {
                rooms[i].get_Parameter(BuiltInParameter.ROOM_NUMBER).Set((i + 1).ToString());
            }
            transaction.Commit();
            return Result.Succeeded;
        }
    }
}
