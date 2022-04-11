namespace Gof
{
    /// <summary>
    /// 责任链
    /// 感觉和visitor有关系
    /// </summary>
    public class Responsibility
    {

    }

    public class ApplyContext
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Hour { get; set; }

        public string Description { get; set; }

        public bool AuditResult { get; set; }
        public string AuditRemark { get; set; }
    }

    public abstract class AbstractAuditor
    {
        public string Name { get; set; }
        public abstract void Audit(ApplyContext context);

        private AbstractAuditor _nexAbstractAuditor = null;

        public void SetNext(AbstractAuditor auditor)
        {
            _nexAbstractAuditor = auditor;
        }

        protected void AuditNext(ApplyContext context)
        {
            this._nexAbstractAuditor?.Audit(context);
        }

    }
    /// <summary>
    /// 1. 审核权限范围内
    /// 2. 超出权限，转交下一环节（主管）审批
    /// </summary>
    public class PM : AbstractAuditor
    {

        public override void Audit(ApplyContext applyContext)
        {
            if (applyContext.Hour <= 8)
            {
                applyContext.AuditResult = true;
                applyContext.AuditRemark = "PM";
            }
            else
            {
                //AbstractAuditor Charge = new Charge(); 
                //Charge.Audit(context);
                base.AuditNext(applyContext);
            }
        }
    }

    public class Charge : AbstractAuditor
    {

        public override void Audit(ApplyContext applyContext)
        {
            if (applyContext.Hour <= 16)
            {
                applyContext.AuditResult = true;
                applyContext.AuditRemark = "PM";
            }
            else
            {
                //AbstractAuditor Charge = new Charge(); 
                //Charge.Audit(context);
                base.AuditNext(applyContext);
            }
        }
    }

    public class Manager : AbstractAuditor
    {
        public override void Audit(ApplyContext context)
        {
            if (context.Hour <= 32)
            {
                context.AuditResult = true;
                context.AuditRemark = "Manager";
            }
            else
            {
                // 流转
                base.AuditNext(context);
            }
        }
    }

    public class Director : AbstractAuditor
    {
        public override void Audit(ApplyContext context)
        {
            if (context.Hour <= 64)
            {
                context.AuditResult = true;
                context.AuditRemark = "Director";
            }
            else
            {
                // 流转
                base.AuditNext(context);
            }
        }
    }
    public class CEO : AbstractAuditor
    {
        public override void Audit(ApplyContext context)
        {
            if (context.Hour <= 128)
            {
                context.AuditResult = true;
                context.AuditRemark = "CEO";
            }
            else
            {
                // 流转
                base.AuditNext(context);
            }
        }
    }
}