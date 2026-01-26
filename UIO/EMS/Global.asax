<%@ Application Language="C#" %>

<script runat="server">



    public void Application_OnStart()
    {
        Application["UsersOnline"] = 0;
    }

    public void Session_OnStart()
    {
        if (Application["UsersOnline"] == null)
        {
            Application["UsersOnline"] = 0; 
        }           
        
       Application.Lock();
        Application["UsersOnline"] = (int)Application["UsersOnline"] + 1;
        Application.UnLock();
    }

    public void Session_OnEnd()
    {
        Application.Lock();
        Application["UsersOnline"] = (int)Application["UsersOnline"] - 1;
        Application.UnLock();
    }


//------------------------------


    void Application_Start(object sender, EventArgs e) 
    {
        Application["UsersOnline"] = 0;
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
    void Application_BeginRequest(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
    }
</script>
