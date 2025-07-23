import jetbrains.buildServer.configs.kotlin.v2019_2.*

object PeopleApiBuild : BuildType({
    id("PeopleApiBuild")
    name = "People API Build"

    vcs {
        root(DslContext.settingsRoot)
    }

    steps {
        step {
            name = "Restore, Build, and Test"
            type = "simpleRunner"
            param("script.content", """
                cd src && \
                dotnet restore People.sln && \
                dotnet build People.sln --configuration Release && \
                dotnet test People.sln --no-build --configuration Release
            """.trimIndent())
        }

        step {
            name = "Docker Build and Push"
            type = "simpleRunner"
            param("script.content", """
                docker build -t people-api:%build.number% .
                docker tag people-api:%build.number% localhost:5000/people-api:%build.number%
                docker push localhost:5000/people-api:%build.number%
            """.trimIndent())
        }
    }

    artifactRules = "image.digest"
})
