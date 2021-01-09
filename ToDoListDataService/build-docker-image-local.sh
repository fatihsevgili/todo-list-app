#!/usr/bin/env bash
SERVICE_NAME="todolistdataservice"
REPOSITORY_TAG=${SERVICE_NAME}":latest"
DOCKER_REPO_ADDRESS="fthsev"
docker build --rm --pull -t ${REPOSITORY_TAG} .
