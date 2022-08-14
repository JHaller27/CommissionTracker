import sys
from pathlib import Path


def populate(base: str, template: str, value: str) -> str:
	old = template.format('')
	new = template.format(value)

	return base.replace(old, new)


_, config, keystore, username, password = sys.argv

config = Path(config)
keystore = Path(keystore)

with config.open('r') as fp:
	config_text = fp.read()

config_text = populate(config_text, 'keystore/release="{}"', keystore)
config_text = populate(config_text, 'keystore/release_user="{}"', username)
config_text = populate(config_text, 'keystore/release_password="{}"', password)


with config.open('w') as fp:
	fp.write(config_text)
