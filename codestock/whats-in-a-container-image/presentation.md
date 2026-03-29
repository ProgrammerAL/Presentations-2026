---
marp: true
title: What's in a Container Image?
paginate: true
theme: default
author: Al Rodriguez
footer: '@ProgrammerAL at programmerAL.com'
---

<style>
section::before {
  /* display: block; */
  content: url('https://raw.githubusercontent.com/ProgrammerAL/Presentations-2026/main/common-images/duende-logo.png');
  width: 10px;
  height: 10px;

  position: absolute;
  right: 60px;
  bottom: -5px;
}
</style>

# What's in a Container Image?

with AL Rodriguez

![bg right 80%](presentation-images/presentation_link_qrcode.png)

---

# Why are we here?

- Mostly Introduction
- Dissect Container **Images**
- Talk Image Security

---

# Shameless Self Promotion

- @ProgrammerAL
- https://ProgrammerAL.com
- Customer Success Engineer
  - Duende
- Freelance Affiliate
  - globalGlob(**/*) aka https://globalGlob.dev

![bg right 80%](presentation-images/presentation_link_qrcode.png)

---

![bg left:70%](presentation-images/vm-infrastructure.webp)
Source: https://www.docker.com/resources/what-container

---
![bg left:70%](presentation-images/container-infrastructure.webp)
Source: https://www.docker.com/resources/what-container

---

```dockerfile
FROM node:20-alpine
LABEL Name="Node.js Demo App" Version=4.9.9
ENV NODE_ENV=production
ENV MY_ENV_VARIABLE=abc123
WORKDIR /app

# For Docker layer caching do this BEFORE copying in rest of app
COPY src/package*.json ./
RUN npm install --production --silent

# NPM is done, now copy in the rest of the project to the workdir
COPY src/. .

# Port 3000 for our Express server 
EXPOSE 3000
ENTRYPOINT ["npm", "start"]
```

---

# Terminology

- Image
  - The File
- Container
  - The Image Running

---

# What is an Image?

- Immutable file
- Layers

---

# Images are built by Layers

- Layers re-used between images
- Recommended Practice: Only include needed layers

---

# What's a Layer?

- Files
  - Binaries, images, text files
- Commands
  - `COPY requirements.txt ./`
  - `RUN pip install --no-cache-dir -r requirements.txt`
- Environment Variables
  - `ENV NODE_VERSION=25.8.0`
  - Plaintext!!!

---

# Interrogate an Image

- UI Tools
  - Docker Desktop
  - Podman
- Run the Image, look at files
  - From UI
  - `docker exec -it <CONTAINER ID> sh`
- Export the file
  - `docker image save --output <path.tar> <image-name>`

---

# Files of an Image

- `oci-layout`
  - Version of the specification for the Image
- `index.json`
  - Points to manifest
- `manifest.json`
  - Lists blobs
- `/blobs`
  - Files for each layer

---

# Demo Exported Image

---

# Security: Only include needed layers/files

- Less Package Vulnerabilities
  - Supply Chain Security
- Smaller Image Downloads

---

# Make Images Smaller

- Slim and/or Hardened Images
- Use Multi-Stage Builds
- Extra Credit: Create a Minimal Image

---

# OS Image Size - Debian Trixie

Normal | Slim
-----|------|
186.4 MB | 119.17 MB

---

# Container Specific OS

- Alpine OS
  - ~5 MB

---

# Distroless Images

- Just the app and dependencies
- https://github.com/GoogleContainerTools/distroless

---

# Hardened Images

- Docker Hardened Images
- Ubuntu Chiselled Images
- chainguard.dev
- minimus.io

---

# Hardened Node 25 Images (March 22, 2026)

- `docker pull node:25-alpine3.22`
  - Alpine 3.22
  - 238.07 MB
  - 6 Vulnerabilities / 175 Packages
* `docker pull cgr.dev/chainguard/node`
  - 233.76 MB
  - 0 Vulnerabilities / 192 Packages
* `docker pull dhi.io/node:25`
  - Docker Hardened / Debian Base
  - 38.49 MB
  - 0 Vulnerabilities / 20 Packages

---

# Extra Credit: Your Own Minimal Image

- Start with `From scratch`
  - `scratch` is reserved, it's empty
  - Not an Image
- Copy in what you need

---

# Multi-Stage Builds

- 2 Images
- 1 to Build/Compile/Do Work
- 1 for output image

---

# Again: Less Files

- Only include needed layers
- Don't copy unnecessary files to the final image
  - `.dockerignore` file or `COPY` commands
- Update dependencies when possible

---

# Demo Time

---

# Other Resources

- https://anniecherkaev.com/what-is-a-container-image
- https://docs.docker.com/get-started/docker-concepts/building-images/understanding-image-layers

---

<style>
  /* use on containing element to display images side by side */
  .inline-images {
      display: flex;
      height: 90%; /* control location on y-axis */
      justify-content: space-between;
      align-items: center;
  }

  .inline-text {
      display: flex;
      justify-content: space-between;
      align-items: center;
      color: var(--h1-color);
      font-size: 1.6em;
      font-weight: var(--base-text-weight-semibold, 600);
  }
</style>

<div class="inline-text">
  <p>Content</p>
  <p>Feedback</p>
</div>

<div class="inline-images">
  <div>

![w:500](presentation-images/presentation_link_qrcode.png)

  </div>
  <div>

![w:500](presentation-images/feedback-qr.png)

  </div>
</div>
