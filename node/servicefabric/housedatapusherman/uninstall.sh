#!/bin/bash

sfctl application delete --application-id housedatapusherman
sfctl application unprovision --application-type-name housedatapushermanType --application-type-version 1.0.0
sfctl store delete --content-path housedatapusherman
