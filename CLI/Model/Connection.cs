using System.ComponentModel.DataAnnotations.Schema;

namespace CLI.Model
{
    [Table("MCPTCONNECTION")]
    public class Connection
    {
        [Column("CONNECTIONID")]
        public int ConnectionId { get; set; }

        [Column("CONNECTIONNAME")]
        public string ConnectionName { get; set; }

        [Column("PARAMETER")]
        public string Parameter { get; set; }

        public override string ToString()
        {
            return $"{nameof(ConnectionName)}: {ConnectionName}, {nameof(Parameter)}: {Parameter}";
        }
    }
}