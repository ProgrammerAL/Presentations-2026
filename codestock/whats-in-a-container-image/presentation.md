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

# About Me

- @ProgrammerAL
- https://ProgrammerAL.com
- Customer Success Engineer at Duende

![bg right 80%](presentation-images/presentation_link_qrcode.png)

---

# Why are we here?

- Dissect Container **Images**
- Mostly Introduction
  - to Image contents

---

# Terminology

- Image
  - The File
- Container
  - The Image Running

---

# What is an Image?

- Immutable
- Layers

---

# Image Layers

- Layers re-used between images
- https://docs.docker.com/get-started/docker-concepts/building-images/understanding-image-layers
- Recommended Practice: Only include needed layers

---

# What's a Layer?

- Commands
  - `COPY requirements.txt ./`
  - `RUN pip install --no-cache-dir -r requirements.txt`
- Environment Variables
  - `ENV NODE_VERSION=25.8.0`

---

# Creating a Minimal Image

- Start with `From scratch`
  - `scratch` is reserved, it's empty
  - Not an Image
- Copy in what you need

---

# Shared Images Example

---

# Export to a file

- ``

---

# Slim vs Non-Slim

- `docker pull node:trixie`
- `docker pull node:trixie-slim`

---

# Trixie Stats

Slim | Non-Slim 
-----|------|
332.91 MB | 1.76 GB
8 Layers | 11 Layers

---

# Security Talk: Package Vulnerabilities

- Images are Immutable
  - Don't get updated
- `Docker Scout` shows packages/vulnerabilities

---

# Hardened Images

- Docker Hardened Images
- Ubuntu Chiselled Images
- chainguard.dev
- minimus.io

---

# Recommended Practices: Consolidated

- Only include needed layers
- Don't copy unnecessary files to the final image
  - `.dockerignore` file or `COPY` commands
- Update dependencies when possible

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
