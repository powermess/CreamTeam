using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

interface IDraggableCharacter
{
    event Action OnCharacterReleased;
}
