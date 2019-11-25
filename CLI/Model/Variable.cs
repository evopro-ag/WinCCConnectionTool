using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model
{
    [Table("MCPTVARIABLEDESC")]
    public class Variable
    {
        [Column("VARIABLEID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VariableId { get; set; }

        [Column("CONNECTIONID")]
        public int ConnectionId { get; set; }

        [Column("VARIABLENAME")]
        public string VariableName { get; set; }

        [Column("PLCBLOCKNAME")]
        public int Blockname { get; set; }

        public override string ToString()
        {
            return $"{nameof(ConnectionId)}: {ConnectionId},{nameof(ConnectionId)}: {VariableName}, {nameof(Blockname)}: {Blockname}";
        }
    }
}
