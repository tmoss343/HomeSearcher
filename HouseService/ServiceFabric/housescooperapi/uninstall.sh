#!/bin/bash

sfctl application delete --application-id housescooperapi
sfctl application unprovision --application-type-name housescooperapiType --application-type-version 1.0.0
sfctl store delete --content-path housescooperapi
