#!/usr/bin/env python3
# -*- coding: utf-8 -*-
import json
import sys
import time
import urllib.request

URL = "https://api.github.com/repos/Nokia1020/Wand-Enhancer/actions/runs?per_page=5"
UA = {"User-Agent": "wand-enhancer-build-poll", "Accept": "application/vnd.github+json"}
seen = None
for _ in range(28):
    try:
        with urllib.request.urlopen(urllib.request.Request(URL, headers=UA), timeout=25) as r:
            data = json.load(r)
        runs = [x for x in data.get("workflow_runs", []) if x.get("name") == "Build executable"]
        if runs:
            run = runs[0]
            tag = (run["status"], run.get("conclusion"), run["head_sha"][:8])
            if tag != seen:
                print("[%s] %s | %s | %s" % (time.strftime("%H:%M:%S"), tag[0], tag[1], tag[2]))
                seen = tag
            if run["status"] == "completed":
                print("URL:", run["html_url"])
                sys.exit(0 if run.get("conclusion") == "success" else 1)
    except Exception as e:
        print("[%s] error: %s" % (time.strftime("%H:%M:%S"), e))
    time.sleep(20)
print("timeout")
sys.exit(2)
