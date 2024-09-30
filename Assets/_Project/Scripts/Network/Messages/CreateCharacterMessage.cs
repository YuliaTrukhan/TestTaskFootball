using _Project.Scripts.ColorSelection.Data;
using Mirror;

namespace _Project.Scripts.Network.Messages
{
    public struct CreateCharacterMessage: NetworkMessage
    {
        public ColorItemData ColorItemData;
    }
}