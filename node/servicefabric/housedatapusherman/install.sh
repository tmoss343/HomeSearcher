#!/bin/bash

sfctl application upload --path housedatapusherman --show-progress
sfctl application provision --application-type-build-path housedatapusherman
sfctl application create --app-name fabric:/housedatapusherman --app-type housedatapushermanType --app-version 1.0.0
