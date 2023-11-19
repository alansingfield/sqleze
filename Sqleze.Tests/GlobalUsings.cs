global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using DryIoc;
global using NSubstitute;
global using NS = NSubstitute;
global using TestCommon.TestUtil;

// In this project we use the real SqlClient
global using MS = Microsoft.Data.SqlClient;
global using MSS = Microsoft.Data.SqlClient.Server;

global using static Sqleze.Tests.TestUtil.TestUtilGlobal;