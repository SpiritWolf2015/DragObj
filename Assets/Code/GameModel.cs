using System.IO;
using System;
using System.Text;
using System.Threading;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class GameModel
{ 
    private static GameModel model;
    public ScenceModel scencemodel;    
    public static GameModel getInstance()
    {
        if (model == null)
        {
            model = new GameModel();

            model.init();
        }
        return model;
    }

    private void init()
    {
        try
        { 
            scencemodel = new ScenceModel();  
			scencemodel.news();
        }
        catch (Exception e)
        {

        }

    }

}
