#!/usr/bin/env bash
cd ClientApp
SERVICE_NAME="todolistuiservice"
REPOSITORY_TAG=${SERVICE_NAME}":latest"
DOCKER_REPO_ADDRESS="fthsev"
docker build --rm --pull -t ${REPOSITORY_TAG} .
docker tag ${REPOSITORY_TAG} ${DOCKER_REPO_ADDRESS}/${SERVICE_NAME}
docker push ${DOCKER_REPO_ADDRESS}/${SERVICE_NAME}