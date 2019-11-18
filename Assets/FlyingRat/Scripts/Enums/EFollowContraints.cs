using System;

namespace FlyingRat
{
    [Flags]
    public enum EFollowContraints
    {
        LockAll = 0x0,

        FollowX = 0x1,

        FollowY = 0x2,

        FollowZ = 0x4,

        FollowAll = FollowX | FollowY | FollowZ
    }
}
