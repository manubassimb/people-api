import jetbrains.buildServer.configs.kotlin.v2019_2.*
import jetbrains.buildServer.configs.kotlin.v2019_2.Project

import PeopleApiBuild

version = "2022.10"

project {
    buildType(PeopleApiBuild)
}