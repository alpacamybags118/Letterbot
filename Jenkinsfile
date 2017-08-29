#!/usr/bin/env groovy

stage('Compile') {
node('windows-docker') {
  try {
      deleteDir()
      checkout scm
      docker.image('alpacamybags\msbuildlite').inside {
      }
    catch(Exception ex) {
        throw ex
    }
  }
}
