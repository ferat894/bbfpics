Imports System.Collections.Generic
Imports System.Web
Imports System.Web.Routing
Imports Microsoft.AspNet.FriendlyUrls

Public Module RouteConfig
    Public Sub RegisterRoutes(routes As RouteCollection)
        Dim settings = New FriendlyUrlSettings()
        settings.AutoRedirectMode = RedirectMode.Permanent

        routes.MapPageRoute("category-romance", "romance", "~/category.aspx?catid=1")
        routes.MapPageRoute("category-birthday", "birthday", "~/category.aspx?catid=2")
        routes.MapPageRoute("category-newbaby", "newbaby", "~/category.aspx?catid=3")
        routes.MapPageRoute("category-justbecause", "justbecause", "~/category.aspx?catid=5")
        routes.MapPageRoute("category-getwell", "getwell", "~/category.aspx?catid=6")
        routes.MapPageRoute("category-sympathy", "sympathy", "~/category.aspx?catid=7")
        routes.MapPageRoute("category-specialoccasion", "specialoccasions", "~/category.aspx?catid=4")
        routes.MapPageRoute("category-special", "special", "~/category.aspx?catid=4")

        routes.MapPageRoute("order", "order/{ItemId}", "~/order.aspx")


        routes.EnableFriendlyUrls(settings)

    End Sub
End Module
