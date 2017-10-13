#!/bin/bash

sfctl application upload --path housescooperapi --show-progress
sfctl application provision --application-type-build-path housescooperapi
sfctl application create --app-name fabric:/housescooperapi --app-type housescooperapiType --app-version 1.0.0
