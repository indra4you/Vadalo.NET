namespace Vadalo.Identity.Providers;

public sealed class IdentityOfEdgeModel(
    string edgeID,
    string fromNodeID,
    string toNodeID,
    IdentityNodeModel fromNode,
    MemberNodeModel toNode
)
{
    public string EdgeID = edgeID;

    public string FromNodeID = fromNodeID;

    public string ToNodeID = toNodeID;

    public IdentityNodeModel FromNode = fromNode;

    public MemberNodeModel ToNode = toNode;
}