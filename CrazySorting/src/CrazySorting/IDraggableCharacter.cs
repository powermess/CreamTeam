﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

interface IDraggableCharacter
{
    void DisableDragging();
    void SetOnMouseUpAction(Action checkIfInGoal);
}
